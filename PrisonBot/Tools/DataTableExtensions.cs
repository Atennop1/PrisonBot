using System.Data;

namespace PrisonBot.Tools
{
    public static class DataTableExtensions
    {
        public static DataTable RightMerge(this IList<DataTable> tables, string primaryKeyColumn)
        {
            if (!tables.Any())
                throw new ArgumentException("Tables must not be empty", nameof(tables));
            
            if (primaryKeyColumn != null && tables.Any(table => !table.Columns.Contains(primaryKeyColumn)))
                throw new ArgumentException("All tables must have the specified primary key column " + primaryKeyColumn, nameof(primaryKeyColumn));

            if( tables.Count == 1)
                return tables[0];

            var table = new DataTable("TblUnion");
            table.BeginLoadData();
            
            foreach (var tableWithWhichMerging in tables) 
                table.Merge(tableWithWhichMerging);
            
            table.EndLoadData();

            if (primaryKeyColumn == null) 
                return table;
            
            var pkGroups = table.AsEnumerable().GroupBy(row => row[primaryKeyColumn]);
            var dupGroups = pkGroups.Where(group => group.Count() > 1);
            
            foreach (var grpDup in dupGroups)
            { 
                var firstRow = grpDup.First();
                foreach (DataColumn column in table.Columns)
                {
                    if (!firstRow.IsNull(column)) 
                        continue;
                    
                    var firstNotNullRow = grpDup.Skip(1).FirstOrDefault(row => !row.IsNull(column));
                    
                    if (firstNotNullRow != null)
                        firstRow[column] = firstNotNullRow[column];
                }
                    
                var rowsToRemove = grpDup.Skip(1);
                foreach(var rowToRemove in rowsToRemove)
                    table.Rows.Remove(rowToRemove);
            }

            return table;
        }
    }
}