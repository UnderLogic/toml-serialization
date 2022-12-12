namespace UnderLogic.Serialization.Toml
{
    internal abstract class TomlValue
    {
        public abstract string ToTomlString();

        public override string ToString() => $"[{GetType().Name}] {ToTomlString()}";
    }
}
