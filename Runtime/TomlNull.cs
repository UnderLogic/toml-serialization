namespace UnderLogic.Serialization.Toml
{
    public sealed class TomlNull : TomlValue
    {
        public static readonly TomlNull Value = new();
        
        private TomlNull() { }
    }
}
