using SqlSugar;

namespace L4d2PanelBackend.Models
{
    public class BaseModel
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Guid id { get; set; } = Guid.NewGuid();
    }
}
