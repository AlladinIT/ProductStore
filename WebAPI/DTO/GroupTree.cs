using WebAPI.Models;

namespace WebAPI.DTO;

public class GroupTree
{
    public Guid GroupId { get; set; }
    public string Name { get; set; }
    public Guid? ParentGroupId { get; set; }
    public List<GroupTree> ChildGroups { get; set; } = new List<GroupTree>();
}