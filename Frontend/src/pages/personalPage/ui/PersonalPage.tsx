import { FC, useEffect } from "react";
import { Button, Card, Col, Divider, Row, Table, Typography } from "antd";
import { useStudent } from "../../../enteties/student/api/useStudent.ts";
import { CopiIcon } from "../../../shared/ui/CopiIcon.tsx";
import { activitiesColumn } from "../model/columns.tsx";
import { useParams } from "react-router-dom";

const PersonalPage: FC = () => {
    const { student, fetchStudent, studentLoading } = useStudent();
    // const { scoreAnalytic, attendanceAnalytic, fetchAnalytic, loading } =
    //     usePersonalAnalytic();
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            fetchStudent(id);
            // fetchAnalytic(id);
        }
    }, []);

    if (!student) {
        return null;
    }

    return (
        <Row gutter={[40, 40]}>
            <Col xs={24}>
                <Typography.Title
                    level={2}
                >{`${student.firstname} ${student.lastname} ${student.patronymic}`}</Typography.Title>
            </Col>
            <Col lg={12} xs={24}>
                <Card title={"Персональна інформація"}>
                    <Typography.Title level={5}>Група</Typography.Title>
                    <Typography.Text>{student.groupCode}</Typography.Text>
                    <Divider />
                    <Typography.Title level={5}>Контакти</Typography.Title>
                    <Typography>
                        <span style={{ width: "20%" }}>Номер телефону:</span>
                        <span style={{ marginLeft: 20 }}>
                            {student.phoneNumber}
                        </span>
                        <CopiIcon data={student.phoneNumber} />
                    </Typography>
                    <Typography>
                        <span style={{ width: "20%" }}>Email:</span>
                        <span style={{ marginLeft: 20 }}>{student.email}</span>
                        <CopiIcon data={student.email} />
                    </Typography>
                    <Divider />
                    <Typography.Title level={5}>Соц мережі</Typography.Title>
                    <Typography></Typography>
                    <Button>Редагувати</Button>
                </Card>
            </Col>
            <Col lg={12} xs={24}>
                <Card title={"Успішність"} style={{ minHeight: "100%" }}>
                    <Table
                        loading={studentLoading}
                        pagination={false}
                        columns={activitiesColumn}
                        dataSource={student.Activities}
                    />
                </Card>
            </Col>
        </Row>
    );
};
export default PersonalPage;
