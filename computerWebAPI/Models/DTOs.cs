namespace computerWebAPI.Models
{
    
        public record CreateOsDTO(string? name);
        public record CreateCompDTO(string? Brand,string? Type,double? Display,int? Memory, Guid OsId);
}
