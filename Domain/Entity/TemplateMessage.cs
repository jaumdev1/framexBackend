namespace Domain.Entity;

public class TemplateMessage
{
        public TemplateMessage(string commandType, object data)
        {
                CommandType = commandType;
                Data = data;
        }
        public string CommandType { get; set; }
        public object Data { get; set; }
        
}