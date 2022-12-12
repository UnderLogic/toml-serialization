namespace UnderLogic.Serialization.Toml.Types
{
    internal abstract class TomlValue : ITomlValue
    {
        public abstract string ToTomlString();

        public override string ToString() => $"[{GetType().Name}] {ToTomlString()}";
    }
}
