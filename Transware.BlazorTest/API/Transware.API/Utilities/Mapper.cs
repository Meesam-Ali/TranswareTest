using Newtonsoft.Json;
using Transware.DB;
using Transware.Entities;

namespace Transware.API.Utilities
{
    public class Mapper
    {
        public static Model.Folder GetFolderModel(Entities.Folder input)
        {
            Model.Folder retVal = null;
            if (input != null)
            {
                retVal = new Model.Folder()
                {
                    Id = input.Id,
                    Name = input.Name
                };
                if (input.FolderId > 0)
                {
                    retVal.ParentFolder = GetFolderModel(DatabaseContext.GetInstance().Folders.FirstOrDefault(x => x.Id == input.FolderId));
                }
            }
            return retVal;
        }

        public static Entities.Folder GetFolderEntities(Model.Folder input)
        {
            Entities.Folder retVal = null;
            if (input != null)
            {
                retVal = new Entities.Folder()
                {
                    Id = input.Id,
                    Name = input.Name,
                    FolderId = input.ParentFolder == null ? 0 : input.ParentFolder.Id
                };
            }
            return retVal;
        }

        public static Entities.Execution GetExecutionEntities(Model.Execution input)
        {
            Entities.Execution retVal = null;
            if (input != null)
            {
                retVal = new Entities.Execution()
                {
                    Id = input.Id,
                    Name = input.Name,
                    Attributes = JsonConvert.DeserializeObject<Dictionary<string, KeyValuePair<DataType, string>>>(JsonConvert.SerializeObject(input.Attributes)),
                    FolderId = input.Folder.Id,
                    EndTime = input.EndTime,
                    StartTime = input.StartTime,
                    Status = (Entities.Status)input.Status,
                    TemplateId = input.Template.Id
                };
            }
            return retVal;
        }

        public static Model.Execution GetExecutionModel(Entities.Execution input)
        {
            Model.Execution retVal = null;
            if (input != null)
            {
                retVal = new Model.Execution()
                {
                    Id = input.Id,
                    Name = input.Name,
                    Attributes = JsonConvert.DeserializeObject<Dictionary<string, KeyValuePair<Model.DataType, string>>>(JsonConvert.SerializeObject(input.Attributes)),
                    Folder = GetFolderModel(DatabaseContext.GetInstance().Folders.FirstOrDefault(x => x.Id == input.FolderId)),
                    EndTime = input.EndTime,
                    StartTime = input.StartTime,
                    Status = (Model.Status)input.Status,
                    Template = GetTemplateModel(DatabaseContext.GetInstance().Templates.FirstOrDefault(x => x.Id == input.TemplateId))
                };
            }
            return retVal;
        }

        public static Model.Template GetTemplateModel(Entities.Template input)
        {
            Model.Template retVal = null;
            if (input != null)
            {
                retVal = new Model.Template()
                {
                    Id = input.Id,
                    Name = input.Name,
                    Attributes = JsonConvert.DeserializeObject<Dictionary<string, Model.DataType>>(JsonConvert.SerializeObject(input.Attributes)),
                    ExecutionTime = input.ExecutionTime,
                    Folder = GetFolderModel(DatabaseContext.GetInstance().Folders.FirstOrDefault(x => x.Id == input.FolderId))

                };
            }
            return retVal;
        }

        public static Entities.Template GetTemplateEntites(Model.Template input)
        {
            Entities.Template retVal = null;
            if (input != null)
            {
                retVal = new Entities.Template()
                {
                    Id = input.Id,
                    Name = input.Name,
                    Attributes = JsonConvert.DeserializeObject<Dictionary<string, DataType>>(JsonConvert.SerializeObject(input.Attributes)),
                    ExecutionTime = input.ExecutionTime,
                    FolderId = input.Folder.Id,
                };
            }
            return retVal;
        }

        public static Model.Result GetResultModel(Entities.Result input)
        {
            Model.Result retVal = null;
            if (input != null)
            {
                retVal = new Model.Result()
                {
                    Id = input.Id,
                    Name = input.Name,
                    Attributes = JsonConvert.DeserializeObject<Dictionary<string, KeyValuePair<Model.DataType, string>>>(JsonConvert.SerializeObject(input.Attributes)),
                    Execution = GetExecutionModel(DatabaseContext.GetInstance().Executions.FirstOrDefault(x => x.Id == input.ExecutionId)),
                    TimeRequired = input.TimeRequired,
                    Folder = GetFolderModel(DatabaseContext.GetInstance().Folders.FirstOrDefault(x => x.Id == input.FolderId)),
                };
            }
            return retVal;
        }

        public static Entities.Result GetResultEntites(Model.Result input)
        {
            Entities.Result retVal = null;
            if (input != null)
            {
                retVal = new Entities.Result()
                {
                    Id = input.Id,
                    Name = input.Name,
                    Attributes = JsonConvert.DeserializeObject<Dictionary<string, KeyValuePair<DataType, string>>>(JsonConvert.SerializeObject(input.Attributes)),
                    ExecutionId = input.Execution.Id,
                    TimeRequired = input.TimeRequired,
                    FolderId = input.Folder.Id,
                };
            }
            return retVal;
        }

    }
}
