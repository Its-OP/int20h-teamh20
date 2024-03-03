import { FC, useEffect } from "react";
import { Button, Form, Input, message, Modal, Popconfirm, Select } from "antd";
import { ModalData } from "../../../shared/model/adminTypes.ts";
import { ModalKey } from "../../../pages/Admin/ui/Admin.tsx";
import { useCreateStudent } from "../../../pages/Auth/api/useCreateStudent.ts";
import { errNotificationMessage } from "../../../shared/model/const.ts";
import { useGroups } from "../../../enteties/groups/api/useGroups.ts";
import { useCreateProfessor } from "../../../pages/Auth/api/useCreateProfessor.ts";
const { useForm } = Form;

const CreateProfessor: FC<ModalData> = ({ open, hideModal }) => {
    const visible = open === ModalKey.CreateProfessor;
    const [form] = useForm();

    const { createUserLoading, createProfessor } = useCreateProfessor();

    const onFinish = async (values: any) => {
        const res = await createProfessor(values);

        if (res) {
            message.success("Викладача успішно додано");
            form.resetFields();
            hideModal();
        } else {
            message.error(errNotificationMessage);
        }
    };

    return (
        <Modal
            footer={null}
            title={"Додати викладача"}
            open={visible}
            onCancel={hideModal}
        >
            <Form form={form} onFinish={onFinish} layout={"vertical"}>
                <div
                    style={{
                        display: "grid",
                        gridTemplateColumns: "repeat(3, 1fr)",
                        gap: "0 40px"
                    }}
                >
                    <Form.Item
                        rules={[{ required: true }]}
                        name={"lastname"}
                        label={"Прізвище"}
                    >
                        <Input />
                    </Form.Item>
                    <Form.Item
                        rules={[{ required: true }]}
                        name={"firstname"}
                        label={"Ім'я"}
                    >
                        <Input />
                    </Form.Item>
                    <Form.Item
                        rules={[{ required: true }]}
                        name={"patronymic"}
                        label={"По батькові"}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item
                        rules={[{ required: true }]}
                        name={"phoneNumber"}
                        label={"Номер телефону"}
                    >
                        <Input type={"tel"} />
                    </Form.Item>
                    <Form.Item
                        rules={[{ required: true }]}
                        name={"email"}
                        label={"Email"}
                    >
                        <Input type={"mail"} />
                    </Form.Item>
                    <Form.Item
                        rules={[{ required: true }]}
                        name={"password"}
                        label={"Пароль"}
                    >
                        <Input type={"password"} />
                    </Form.Item>
                </div>
                <div style={{ display: "flex", justifyContent: "flex-end" }}>
                    <Popconfirm
                        title={"Ви впевнені що хочете додати викладача до системи ?"}
                        onConfirm={form.submit}
                        disabled={createUserLoading}
                    >
                        <Button loading={createUserLoading} type={"primary"}>
                            Додати
                        </Button>
                    </Popconfirm>
                </div>
            </Form>
        </Modal>
    );
};
export default CreateProfessor;
