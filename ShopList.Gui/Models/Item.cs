using SQLite;
using System.ComponentModel;

namespace ShopList.Gui.Models
{
    [Table("ShopList")]
    public class Item
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Nombre { get; set; } = string.Empty;
        [DefaultValue(1)]
        [NotNull]
        public int Cantidad { get; set; }
        [NotNull]
        public bool Comprado { get; set; } = false;

        public override string ToString()
        {
            return $"{Nombre} ({Cantidad})";
        }
     
    }
}
