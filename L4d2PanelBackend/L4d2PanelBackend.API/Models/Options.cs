using SqlSugar;

namespace L4d2PanelBackend.API.Models
{
    [SugarTable("Options")]
    public class Options
    {
        public Options(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        [SugarColumn(Length = 200)]
        public string name { get; set; }

        [SugarColumn(Length = 200, IsNullable = true)]
        public string? value { get; set; }
    }
}
