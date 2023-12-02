namespace WebAPI.Models;

public class Group
{
    public Guid GroupId { get; set; }
    public String GroupName { get; set; }
    public Guid? ParentGroupId { get; set; }
    public Group ParentGroup { get; set; }
}