namespace Anberkada.MusicBase.Biz.Contracts
{
    /// <summary>
    /// Interface for chords.
    /// </summary>
    public interface IChord : IHarmonicStack
    {
        /// <summary>
        /// Gets the chord.
        /// </summary>
        /// <value>
        /// The chord.
        /// </value>
        Chords Chord { get; }
    }
}