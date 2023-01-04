namespace UnderLogic.Serialization.Toml
{
    internal struct ConvertOptions
    {
        public static readonly ConvertOptions Default = MakeDefaults();
        
        public bool ForceArray { get; set; }
        public bool ForceList { get; set; }
        public bool ForceInline { get; set; }
        public bool ForceExpand { get; set; }
        public bool IsLiteral { get; set; }
        public bool IsMultiline { get; set; }
        public StringCasing Casing { get; set; }
        public NumberFormat NumberFormat { get; set; }

        private static ConvertOptions MakeDefaults()
        {
            return new ConvertOptions
            {
                ForceArray = false,
                ForceList = false,
                Casing = StringCasing.Default,
                NumberFormat = NumberFormat.Decimal
            };
        }
    }
}
