namespace UnderLogic.Serialization.Toml
{
    internal abstract class TomlValue
    {
        public static readonly TomlValue Null = null;
        
        public abstract string ToTomlString();

        public override string ToString() => $"[{GetType().Name}] {ToTomlString()}";
    }
}
