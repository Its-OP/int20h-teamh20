import { FC, useState } from "react";
import { Button, Card, Col, Collapse, Row, Space, Typography } from "antd";
import { CreateStudent } from "../../../features/createSudent";
import CreateGroup from "../../../features/createGroup/ui/CreateGroup.tsx";
import CreateActivityType from "../../../features/createActivityType/ui/CreateActivityType.tsx";
import { CreateNotification } from "../../../features/createNotification";

export enum ModalKey {
    Defult,
    CreateStudent,
    CreateActivity,
    CreateGroup,
    CreateDiscipline,
    CreateNotification
}

const { Panel } = Collapse;

const Admin: FC = () => {
    const [openModal, setOpenModal] = useState<ModalKey>(ModalKey.Defult);

    const closeModal = () => setOpenModal(ModalKey.Defult);

    const showModal = (key: ModalKey) => setOpenModal(key);
    return (
        <>
            <Typography.Title>Адмін панель</Typography.Title>
            <Card>
                <Row gutter={[40, 40]}>
                    <Col xs={6}>
                        <Collapse defaultActiveKey={1}>
                            <Panel key={1} header={"Студенти"}>
                                <Space
                                    style={{ width: "100%" }}
                                    direction={"vertical"}
                                >
                                    <Button
                                        onClick={() =>
                                            showModal(ModalKey.CreateStudent)
                                        }
                                        style={{ width: "100%" }}
                                        type={"primary"}
                                    >
                                        Створити студента
                                    </Button>
                                </Space>
                            </Panel>
                        </Collapse>
                    </Col>
                    <Col xs={6}>
                        <Collapse defaultActiveKey={1}>
                            <Panel key={1} header={"Активності"}>
                                <Space
                                    style={{ width: "100%" }}
                                    direction={"vertical"}
                                >
                                    <Button
                                        onClick={() =>
                                            showModal(ModalKey.CreateActivity)
                                        }
                                        style={{ width: "100%" }}
                                        type={"primary"}
                                    >
                                        Додати активність
                                    </Button>
                                </Space>
                            </Panel>
                        </Collapse>
                    </Col>
                    <Col xs={6}>
                        <Collapse defaultActiveKey={1}>
                            <Panel key={1} header={"Групи"}>
                                <Space
                                    style={{ width: "100%" }}
                                    direction={"vertical"}
                                >
                                    <Button
                                        onClick={() =>
                                            showModal(ModalKey.CreateGroup)
                                        }
                                        style={{ width: "100%" }}
                                        type={"primary"}
                                    >
                                        Створити группу
                                    </Button>
                                    <Button
                                        onClick={() =>
                                            showModal(ModalKey.CreateDiscipline)
                                        }
                                        style={{ width: "100%" }}
                                        type={"primary"}
                                    >
                                        Створити предмет
                                    </Button>
                                </Space>
                            </Panel>
                        </Collapse>
                    </Col>

                    <Col xs={6}>
                        <Collapse defaultActiveKey={1}>
                            <Panel key={1} header={"Повідомлення"}>
                                <Space
                                    style={{ width: "100%" }}
                                    direction={"vertical"}
                                >
                                    <Button
                                        onClick={() =>
                                            showModal(
                                                ModalKey.CreateNotification
                                            )
                                        }
                                        style={{ width: "100%" }}
                                        type={"primary"}
                                    >
                                        Створити розсилку
                                    </Button>
                                </Space>
                            </Panel>
                        </Collapse>
                    </Col>
                </Row>
            </Card>
            <CreateStudent open={openModal} hideModal={closeModal} />
            <CreateGroup open={openModal} hideModal={closeModal} />
            <CreateActivityType open={openModal} hideModal={closeModal} />
            <CreateNotification open={openModal} hideModal={closeModal} />
        </>
    );
};

export default Admin;
