namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlNull : TomlValue
    {
        public static readonly TomlNull Value = new TomlNull();
        
        private TomlNull() { }

        public override string ToString() => "null";
    }
}
