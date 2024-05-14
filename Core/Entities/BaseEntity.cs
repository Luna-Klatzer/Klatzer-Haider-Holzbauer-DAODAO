using Core.Contracts;

namespace Core.Entities;

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
}