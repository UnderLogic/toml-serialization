namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlNull : TomlValue
    {
        public static readonly TomlNull Value = new();
        
        private TomlNull() { }
    }
}
