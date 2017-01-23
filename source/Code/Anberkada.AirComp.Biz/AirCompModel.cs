
namespace Anberkada.AirComp.Biz
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Leap;

    using Contracts;
    
    /// <summary>
    /// Model for touchless interactions with Leap Motion.
    /// </summary>
    public class AirCompModel : CompModelBase, IDevCompModel<Frame>
    {
        private static DateTime _lastNoteOnDateTime = DateTime.Now;
        private static Vector _lastTranslationVectorOfPitchPointable = new Vector(0f, 0f, 0f);
        private static Vector _centerPositionForPitchBend = new Vector(0f, 0f, 0f);
        private static Vector _noteOnPosition = new Vector(0f, 0f, 0f);
        private Controller _controller;
        private Listener _listener;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AirCompModel" /> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="outDevices">The list of out devices.</param>
        /// <param name="outChannels">The list of out channels.</param>
        public AirCompModel(CompConfig config, IEnumerable<string> outDevices, IEnumerable<int> outChannels)
            : base(config, outDevices, outChannels)
        {
        }

        #region Implementation of IDevCompModel

        /// <summary>
        /// Initializes the controller device of this instance.
        /// </summary>
        public void InitializeControllerDevice()
        {
            // Create config, listener and controller
            _listener = new TouchlessListener(this);
            _controller = new TouchlessController();

            // Have the listener receive events from the leap controller
            _controller.AddListener(_listener);
        }

        /// <summary>
        /// Updates from controller device.
        /// </summary>
        /// <param name="deviceFrame">The device frame.</param>
        public void UpdateFromControllerDevice(Frame deviceFrame)
        {
            lock (Lock)
            {
                if (deviceFrame == null)
                {
                    return;
                }

                // All notes off, if 2 hands raised
                var modeControlHand = GetModeControlHand(deviceFrame.Hands);
                if (modeControlHand != null && TryHandleAllNotesOff(modeControlHand))
                {
                    return;
                }

                // Control amplitude with left hand in y-direction:
                UpdateAmplitude(GetAmplitudeHand(deviceFrame.Hands));

                // Control pitch with optional right hand in y-direction:
                UpdatePitch(GetPitchHand(deviceFrame.Hands));

                // Control expression/modulation with optional right hand in x-direction:
                UpdateExpression(GetExpressionHand(deviceFrame.Hands));

                // Switch off current note?
                if (PlayingPitch != null)
                {
                    if (TryHandleNoteOff(deviceFrame))
                    {
                        return;
                    }
                }

                var pitchBendPointable = GetPitchBendPointable(deviceFrame.Hands);

                // Try to handle note on gesture: Right handed finger tip
                if (TryHandleNoteOn(deviceFrame) && pitchBendPointable != null)
                {
                    // Pitch bend will be reset with each note on
                    ResetPitchBend(pitchBendPointable);
                }

                if (pitchBendPointable != null)
                {
                    UpdatePitchBend(pitchBendPointable);
                }
            }
        }

        #endregion // Implementation of IDevCompModel

        #region Implementation of IDispoable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion // Implementation of IDispoable

        #region Protected methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_controller != null)
                {
                    // Remove the listener when done
                    _controller.RemoveListener(_listener);
                    _controller.Dispose();

                    _listener.Dispose();
                }
            }
            // Free unmanaged resources, if there are any
        }

        /// <summary>
        /// Gets the pitch hand.
        /// </summary>
        /// <param name="handList">The hand list.</param>
        /// <returns>Detected hand for controlling pitch or null.</returns>
        protected virtual Hand GetPitchHand(HandList handList)
        {
            if (handList.Count < 1)
            {
                return null;
            }

            // Right hand is controlling pitch
            return handList.Rightmost;
        }

        /// <summary>
        /// Gets the amplitude hand.
        /// </summary>
        /// <param name="handList">The hand list.</param>
        /// <returns>Detected hand for controlling amplitude or null.</returns>
        protected virtual Hand GetAmplitudeHand(HandList handList)
        {
            if (handList.Count < 2)
            {
                return null;
            }
            
            // Left hand is controlling amplitude
            return handList.Leftmost;
        }

        /// <summary>
        /// Gets the expression hand.
        /// </summary>
        /// <param name="handList">The hand list.</param>
        /// <returns>Detected hand for controlling expression or null.</returns>
        protected virtual Hand GetExpressionHand(HandList handList)
        {
            if (handList.Count < 2)
            {
                return null;
            }

            // Left hand is controlling expression
            return handList.Leftmost;
        }

        /// <summary>
        /// Gets the hand for controlling pitch bend.
        /// </summary>
        /// <param name="handList">The hand list.</param>
        /// <returns>Detected hand for controlling pitch bend or null.</returns>
        protected virtual Hand GetPitchBendHand(HandList handList)
        {
            if (handList.Count < 1)
            {
                return null;
            }

            // Right hand is controlling pitch bend
            return handList.Rightmost;
        }

        /// <summary>
        /// Gets the hand for controlling the application mode.
        /// </summary>
        /// <param name="handList">The hand list.</param>
        /// <returns>Detected hand for controlling application mode.</returns>
        protected virtual Hand GetModeControlHand(HandList handList)
        {
            if (handList.Count < 2)
            {
                return null;
            }

            // Left hand is controlling app mode
            return handList.Leftmost;
        }

        /// <summary>
        /// Gets the pointable (finger) to control pitch bend.
        /// </summary>
        /// <param name="handList">The hand list.</param>
        /// <returns>Detected pointable or null.</returns>
        protected virtual Pointable GetPitchBendPointable(HandList handList)
        {
            var pitchBendHand = GetPitchBendHand(handList);
            if (pitchBendHand != null && pitchBendHand.Pointables.Count > 0)
            {
                return pitchBendHand.Pointables[0];
            }

            return null;
        }

        #endregion // Protected methods

        #region Private methods

        /// <summary>
        /// Resets the pitch bend center point.
        /// </summary>
        /// <param name="pitchBendPointable">The pitch bend pointable.</param>
        private void ResetPitchBend(Pointable pitchBendPointable)
        {
            _centerPositionForPitchBend = pitchBendPointable.StabilizedTipPosition;
        }

        /// <summary>
        /// Updates the pitch bend.
        /// </summary>
        /// <param name="pitchBendPointable">The pitch bend pointable.</param>
        private void UpdatePitchBend(Pointable pitchBendPointable)
        {
            // Determine pitch bend by x position of the fingertip relative to the center point (set by last note on event)
            var translation = pitchBendPointable.StabilizedTipPosition - _centerPositionForPitchBend;
            CurrentPitchBend = NormalizeDoubleValue(translation.x, -Config.PitchBendMaxRange / 2, Config.PitchBendMaxRange / 2);
        }

        /// <summary>
        /// Tries to handle the note on gesture.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns>True, if gesture was handled; otherwise false</returns>
        private bool TryHandleNoteOn(Frame frame)
        {
            var pitchHand = GetPitchHand(frame.Hands);
            if (pitchHand == null)
            {
                return false;
            }

            if (pitchHand.IsValid && pitchHand.Pointables.Count >= 1)
            {
                var pitchPointable = pitchHand.Pointables[0];
                var velocity = pitchPointable.TipVelocity;
                
                // Check conditions for note on trigger
                var currentTime = DateTime.Now;
                if (velocity.z < -Config.TouchResponseMinVelocity                                      // z velocity for note on is negative!
                    && (currentTime.Subtract(_lastNoteOnDateTime) > Config.NoteOnRetriggerMinTimeSpan) // ensure min timespan between 2 note on events to avoid chatter
                    && velocity.z > _lastTranslationVectorOfPitchPointable.z                           // note on trigger after acceleration decreases
                    )
                {
                    var noteOnVelocity = (_lastTranslationVectorOfPitchPointable.z + Config.TouchResponseMinVelocity) / (Config.TouchResponseMaxVelocity - Config.TouchResponseMinVelocity);
                    noteOnVelocity = LimitDoubleValue(-noteOnVelocity, Config.TouchResponseMinValue, Config.TouchResponseMaxValue);
                    _lastNoteOnDateTime = currentTime;
                    _noteOnPosition = pitchHand.PalmPosition;
                    Debug.WriteLine("Note on: {0}, at position: {1}", CurrentPitch, _noteOnPosition);
                    OnNoteOnEvent(new NoteEventArgs(CurrentPitch, noteOnVelocity));
                    
                    return true;
                }

                _lastTranslationVectorOfPitchPointable = velocity;
            }

            return false;
        }

        /// <summary>
        /// Tries to handle the note off gesture.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns>True, if gesture was handled; otherwise false</returns>
        private bool TryHandleNoteOff(Frame frame)
        {
            var pitchHand = GetPitchHand(frame.Hands);
            if (pitchHand == null)
            {
                return false;
            }

            if (pitchHand.IsValid)
            {
                var currentPosition = pitchHand.PalmPosition;
                var distance = currentPosition.DistanceTo(_noteOnPosition);

                if (distance > DistanceForNoteOffTrigger)
                {
                    Debug.WriteLine("DistanceForNoteOffTrigger: {0}", DistanceForNoteOffTrigger); //bs TODO
                    Debug.WriteLine("Note off: {0}, at position: {1}, distance: {2}", PlayingPitch, currentPosition, distance);
                    OnNoteOffEvent(new NoteEventArgs(PlayingPitch, 1.0f));

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Tries to handle the all notes off gesture.
        /// </summary>
        /// <param name="modeControlHand">The mode control hand.</param>
        /// <returns>True, if gesture was handled; otherwise false</returns>
        private bool TryHandleAllNotesOff(Hand modeControlHand)
        {
            if (!modeControlHand.IsValid || modeControlHand.Pointables.Count < 2)
            {
                return false;
            }

            if (modeControlHand.Direction.y > Config.ModeControlYDirectionThreshold)
            {
                OnAllNotesOffEvent();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the pitch.
        /// </summary>
        /// <param name="pitchHand">The pitch hand.</param>
        private void UpdatePitch(Hand pitchHand)
        {
            if (pitchHand == null)
            {
                return;
            }

            // Determine pitch within current scale due to relative y position within bounding interaction box
            double normalizedPosition = NormalizeDoubleValue(pitchHand.StabilizedPalmPosition.y, Config.PitchMinPosition,
                Config.PitchMaxPosition);
            CurrentPitch = GetPitchByNormalizedPosition(normalizedPosition);
        }

        /// <summary>
        /// Updates the amplitude.
        /// </summary>
        /// <param name="amplitudeHand">The amplitude hand.</param>
        private void UpdateAmplitude(Hand amplitudeHand)
        {
            if (amplitudeHand == null)
            {
                CurrentAmplitude = Config.AmplitudeDefaultValue;
                return;
            }

            var position = amplitudeHand.StabilizedPalmPosition.y;
            var amplitude = (position - Config.AmplitudeMinPosition) / (Config.AmplitudeMaxPosition - Config.AmplitudeMinPosition);
            CurrentAmplitude = LimitDoubleValue(amplitude, Config.AmplitudeMinValue, Config.AmplitudeMaxValue);
        }

        /// <summary>
        /// Updates the expression.
        /// </summary>
        /// <param name="expressionHand">The expression hand.</param>
        private void UpdateExpression(Hand expressionHand)
        {
            if (expressionHand == null)
            {
                CurrentExpression = Config.ExpressionDefaultValue;
                return;
            }

            var position = expressionHand.StabilizedPalmPosition.x;
            var expression = (position - Config.ExpressionMinPosition) / (Config.ExpressionMaxPosition - Config.ExpressionMinPosition);
            CurrentExpression = LimitDoubleValue(expression, Config.ExpressionMinValue, Config.ExpressionMaxValue);
        }

        #endregion // Private methods
    }
}
