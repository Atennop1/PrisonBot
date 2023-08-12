using System.Data;

namespace PrisonBot.Functional
{
    public sealed class InformationStringFactory
    {
        public string GetFor(DataTable informationTable)
        {
            return $"ИМЯ: {((string)informationTable.Rows[0]["name"]).ToUpper()}\n" +
                   $"КЛИКУХА: {((string)informationTable.Rows[0]["nickname"]).ToUpper()}\n" +
                   $"СКОКО ЛЕТ В ЗОНЕ: {((int)informationTable.Rows[0]["years_in_prison"]).ToString().ToUpper()}\n" + 
                   $"СОЦИАЛЬНЫЙ РЕЙТИНГ: {((int)informationTable.Rows[0]["social_credit"]).ToString().ToUpper()}%\n" +
                   $"НАПРАВЛЕННОСТЬ: {((string)informationTable.Rows[0]["specialization"]).ToUpper()}\n" +
                   $"УРОВЕНЬ НАПРАВЛЕННОСТИ: {((string)informationTable.Rows[0]["specialization_level"]).ToUpper()}\n" +
                   $"СТАТУС: {GetStatus((int)informationTable.Rows[0]["status_id"]).ToUpper()}\n" + 
                   $"В МКС С: {((string)informationTable.Rows[0]["join_date"]).ToUpper()}\n" + 
                   $"БЫЛ ЗАМЕЧЕН ЗА ПИТОНИЗМОМ: {((string)informationTable.Rows[0]["was_in_python"]).ToUpper()}\n" +
                   $"УПОМЯНАЛСЯ В БУГУРТАХ: {((string)informationTable.Rows[0]["was_in_bugurts"]).ToUpper()}";
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