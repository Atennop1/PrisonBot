using System.Data;

namespace PrisonBot.Functional
{
    public sealed class InformationStringFactory
    {
        public string GetFor(DataTable table)
        {
            return $"ИМЯ: {((string)table.Rows[0]["name"]).ToUpper()}\n" +
                   $"КЛИКУХА: {((string)table.Rows[0]["nickname"]).ToUpper()}\n" +
                   $"СКОКО ЛЕТ В ЗОНЕ: {((string)table.Rows[0]["years_in_prison"]).ToUpper()}\n" + 
                   $"СОЦИАЛЬНЫЙ РЕЙТИНГ: {((string)table.Rows[0]["social_credit"]).ToUpper()}%\n" +
                   $"НАПРАВЛЕННОСТЬ: {((string)table.Rows[0]["specialization"]).ToUpper()}\n" +
                   $"УРОВЕНЬ НАПРАВЛЕННОСТИ: {((string)table.Rows[0]["specialization_level"]).ToUpper()}\n" +
                   $"СТАТУС: {GetStatus((int)table.Rows[0]["status_id"]).ToUpper()}\n" + 
                   $"В МКС С: {((string)table.Rows[0]["join_date"]).ToUpper()}\n" + 
                   $"БЫЛ ЗАМЕЧЕН ЗА ПИТОНИЗМОМ: {((string)table.Rows[0]["was_in_python"]).ToUpper()}\n" +
                   $"УПОМЯНАЛСЯ В БУГУРТАХ: {((string)table.Rows[0]["was_in_bugurts"]).ToUpper()}";
        }

        private string GetStatus(int id)
        {
            return id switch
            {
                0 => "опущенный",
                1 => "лох",
                2 => "хромой",
                3 => "мужик",
                4 => "пацан",
                5 => "блатной",
                6 => "пахан",
                7 => "барон",
                8 => "вор в законе",
                9 => "легенда зоны",
                _ => "неизвестен"
            };
        }
    }
}