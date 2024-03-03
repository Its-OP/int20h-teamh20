import { TableProps, Typography } from "antd";
import { Link } from "react-router-dom";
import { StudentSimple } from "../../../shared/model/types.ts";

export const studentsColumns: TableProps["columns"] = [
    {
        dataIndex: "firstname",
        title: "ПІБ",
        render(_: string, record: StudentSimple) {
            return (
                <Link
                    to={"/student/" + record.id}
                >{`${record.firstname} ${record.lastname} ${record.patronymic}`}</Link>
            );
        }
    },
    {
        dataIndex: "averageScore",
        title: "Середній бал",
        sorter: (a, b) => a.averageScore - b.averageScore,
        render(_: number, record: StudentSimple) {
            return <Typography.Text>{record.averageScore}</Typography.Text>;
        }
    },
    {
        dataIndex: "presences",
        title: "Відвідуваність",
        sorter: (a: StudentSimple, b: StudentSimple) =>
            a.presences / (a.absences + a.presences) -
            b.presences / (b.absences + b.presences),
        render(_: string, record: StudentSimple) {
            return (
                <Typography.Text>{`${record.presences}/${record.presences + record.absences}`}</Typography.Text>
            );
        }
    }
];
