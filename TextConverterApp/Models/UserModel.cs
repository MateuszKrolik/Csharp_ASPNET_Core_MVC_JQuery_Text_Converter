namespace TextConverterApp.Models;

public class UserModel
{
    public Guid? Id { get; set; }
    public List<ConversionModel>? Conversions { get; set; }
}