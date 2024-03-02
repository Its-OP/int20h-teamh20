namespace backend.ApiContracts;

/*
Groupe:
    properties:
        code:
            type: string
        subjects:
            type: array
            items:
                $ref: Subject
        students:
            type: array
            items:
                $ref: SimpleStudent
*/

public class GroupContract(domain.Group group)
{
    public string Code { get; set; } = group.Code;

    // TODO: subjects
    // TODO: students
}