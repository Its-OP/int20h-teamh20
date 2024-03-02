import { FC } from "react";
import { Button, Card, Col, Divider, Row, Table, Typography } from "antd";
import { useStudent } from "../../../enteties/student/api/useStudent.ts";
import classes from "./styles.module.css";
import { CopiIcon } from "../../../shared/ui/CopiIcon.tsx";
import { activitiesColumn } from "../model/columns.tsx";
const PersonalPage: FC = () => {
    const { student, fetchStudent, studentLoading } = useStudent();

    if (!student) {
        return null;
    }
    console.log(student);
    return (
        <Row gutter={[40, 40]}>
            <Col xs={24}>
                <Typography.Title
                    level={2}
                >{`${student.firstname} ${student.lastname} ${student.patronymic}`}</Typography.Title>
            </Col>
            <Col xs={12}>
                <Card title={"Персональна інформація"}>
                    <Typography.Title level={5}>Група</Typography.Title>
                    <Typography.Text>{student.subject}</Typography.Text>
                    <Divider />
                    <Typography.Title level={5}>Контаки</Typography.Title>
                    <Typography>
                        <span style={{ width: "20%" }}>Номер телефону:</span>
                        <span style={{ marginLeft: 20 }}>
                            {student.mobileNumber}
                        </span>
                        <CopiIcon data={student.mobileNumber} />
                    </Typography>
                    <Typography>
                        <span style={{ width: "20%" }}>Email:</span>
                        <span style={{ marginLeft: 20 }}>{student.email}</span>
                        <CopiIcon data={student.mobileNumber} />
                    </Typography>
                    <Divider />
                    <Typography.Title level={5}>Соц мережі</Typography.Title>
                    <Typography></Typography>
                    <Button>Редагувати</Button>
                </Card>
            </Col>
            <Col xs={12}>
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
