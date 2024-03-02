import { Checkbox, TableProps, Typography } from "antd";
import { formater } from "../../../shared/model/dateTimeFormatter.ts";

export const activitiesColumn: TableProps["columns"] = [
    {
        title: "Предмет",
        dataIndex: "subject"
    },
    {
        dataIndex: "presence",
        title: "Присутність",
        render(value) {
            console.log(value);
            return <Checkbox checked={value} />;
        }
    },
    {
        dataIndex: "grade",
        title: "Середній бал",
        render(value) {
            return (
                <Typography.Text
                    type={
                        value >= 90
                            ? "success"
                            : value <= 40
                              ? "danger"
                              : undefined
                    }
                >
                    {value}
                </Typography.Text>
            );
        }
    },
    {
        dataIndex: "date",
        title: "Дата",
        render(value) {
            return (
                <Typography.Text>{formater(new Date(value))}</Typography.Text>
            );
        }
    }
];
