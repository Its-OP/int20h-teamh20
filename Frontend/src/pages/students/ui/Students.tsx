import { FC, useEffect, useState } from "react";
import { Col, Row, Select, Table, Typography } from "antd";
import { useGroups } from "../../../enteties/groups/api/useGroups.ts";
import { studentsColumns } from "../models/columns.tsx";
import { useStudents } from "../../../enteties/students/api/useStudents.ts";

const Students: FC = () => {
    const { groups, groupsLoading, fetchGroups } = useGroups();
    const { students, fetchStudents, studentsLoading, clearStudents } =
        useStudents();
    const [group, setGroup] = useState<string>();

    useEffect(() => {
        fetchGroups();
    }, []);

    useEffect(() => {
        if (group) {
            fetchStudents(group);
        } else {
            clearStudents();
        }
    }, [group]);

    return (
        <>
            <Row gutter={[40, 40]}>
                <Col xs={24}>
                    <Typography.Title level={1}>
                        Пошук студентів
                    </Typography.Title>
                    <Typography.Text>Для пошуку оберіть групу</Typography.Text>
                </Col>
                <Col xs={24}>
                    <Select
                        options={groups.map(group => ({
                            value: group.id,
                            label: group.code
                        }))}
                        onChange={setGroup}
                        value={group}
                        placeholder={"Группа"}
                        style={{ minWidth: 200 }}
                        loading={groupsLoading}
                        allowClear
                    />
                </Col>
                {group && (
                    <Col xs={24}>
                        <Table
                            loading={studentsLoading}
                            dataSource={students}
                            columns={studentsColumns}
                            pagination={false}
                        />
                    </Col>
                )}
            </Row>
        </>
    );
};
export default Students;
