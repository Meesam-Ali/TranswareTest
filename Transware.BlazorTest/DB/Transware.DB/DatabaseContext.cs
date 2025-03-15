using Newtonsoft.Json;
using Transware.Entities;

namespace Transware.DB
{
    public class DatabaseContext
    {
        private static DatabaseContext? instance;
        public List<Folder> Folders { get; set; }
        public List<Template> Templates { get; set; }
        public List<Execution> Executions { get; set; }
        public List<Result> Results { get; set; }

        private DatabaseContext()
        {
            Folders = JsonConvert.DeserializeObject<List<Folder>>(File.ReadAllText(@"..\..\DB\Transware.DB\Folders.json"));
            Templates = JsonConvert.DeserializeObject<List<Template>>(File.ReadAllText(@"..\..\DB\Transware.DB\Templates.json"));
            Executions = new();
            Results = new();

        }

        public static DatabaseContext GetInstance()
        {
            if (instance == null)
            {
                instance = new DatabaseContext();
            }
            UpdateStates();
            return instance;
        }

        private static void UpdateStates()
        {
            foreach (Execution item in instance.Executions)
            {
                if (item.Status == Status.Running && item.EndTime < DateTime.UtcNow)
                {
                    item.Status = Status.Finished;
                    var result = new Result();
                    result.Attributes = item.Attributes;
                    result.ExecutionId = item.Id;
                    result.FolderId = item.FolderId;
                    result.Name = item.Name + "-result";
                    result.TimeRequired = (item.EndTime - item.StartTime).TotalMinutes;
                    result.Id = instance.Results.Count > 0 ? instance.Results.Max(x => x.Id) + 1 : 1;
                    instance.Results.Add(result);
                }
            }
        }

        public static void Reset()
        {
            instance = new DatabaseContext();
        }


    }


}
