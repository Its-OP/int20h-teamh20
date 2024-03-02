using domain;

namespace backend.ApiContracts;

public class GroupContract
{
    public string Code { get; set; }
    public IEnumerable<SubjectContract> Subjects { get; set; } = new List<SubjectContract>();
}