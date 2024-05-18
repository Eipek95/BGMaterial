namespace BGMaterial.Application.Dtos
{
    public class CreateMaterialDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Model { get; set; }
        public string? Engine { get; set; }
        public string? Year { get; set; }
        public decimal ListPrice { get; set; }
        public int StockMrk { get; set; }
        public int StockLzm { get; set; }
        public int StockAnk { get; set; }
        public int StockAdn { get; set; }
        public int StockErz { get; set; }
    }
}
