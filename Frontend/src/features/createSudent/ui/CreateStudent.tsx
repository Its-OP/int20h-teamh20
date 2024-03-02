import { FC } from "react";
import { Button, Form, Input, Modal, Popconfirm, Select } from "antd";
import { ModalData } from "../../../shared/model/adminTypes.ts";
import { ModalKey } from "../../../pages/Admin/ui/Admin.tsx";
import { useCreateStudent } from "../../../pages/Auth/api/useCreateStudent.ts";
const { useForm } = Form;
const CreateStudent: FC<ModalData> = ({ open, hideModal }) => {
    const visible = open === ModalKey.CreateStudent;
    const [form] = useForm();

    const { createUserLoading, createStudent } = useCreateStudent();
    return (
        <Modal
            footer={null}
            title={"Створення студента"}
            open={visible}
            onCancel={hideModal}
        >
            <Form form={form} onFinish={createStudent} layout={"vertical"}>
                <div
                    style={{
                        display: "grid",
                        gridTemplateColumns: "repeat(3, 1fr)",
                        gap: "0 40px"
                    }}
                >
                    <Form.Item name={"lastname"} label={"Прізвище"}>
                        <Input />
                    </Form.Item>
                    <Form.Item name={"firstname"} label={"Ім'я"}>
                        <Input />
                    </Form.Item>
                    <Form.Item name={"patronymic"} label={"По батькові"}>
                        <Input />
                    </Form.Item>

                    <Form.Item name={"mobileNumber"} label={"Номер телефону"}>
                        <Input type={"tel"} />
                    </Form.Item>
                    <Form.Item name={"email"} label={"Email"}>
                        <Input type={"mail"} />
                    </Form.Item>
                    <Form.Item name={"subject"} label={"Група"}>
                        <Select />
                    </Form.Item>
                </div>
                <div style={{ display: "flex", justifyContent: "flex-end" }}>
                    <Popconfirm
                        title={"Ви впевнені що хочете створити студента ?"}
                        onConfirm={form.submit}
                    >
                        <Button type={"primary"}>Створити</Button>
                    </Popconfirm>
                </div>
            </Form>
        </Modal>
    );
};
export default CreateStudent;
