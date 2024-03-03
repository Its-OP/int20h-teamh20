import { FC, useEffect } from "react";
import { Button, Form, Input, message, Modal, Popconfirm, Select } from "antd";
import { ModalData } from "../../../shared/model/adminTypes.ts";
import { ModalKey } from "../../../pages/Admin/ui/Admin.tsx";
import { useCreateStudent } from "../../../pages/Auth/api/useCreateStudent.ts";
import { errNotificationMessage } from "../../../shared/model/const.ts";
import { useGroups } from "../../../enteties/groups/api/useGroups.ts";
const { useForm } = Form;

const CreateStudent: FC<ModalData> = ({ open, hideModal }) => {
    const visible = open === ModalKey.CreateStudent;
    const [form] = useForm();

    const { createUserLoading, createStudent } = useCreateStudent();
    const { groups, fetchGroups, groupsLoading } = useGroups();

    useEffect(() => {
        if (visible) {
            fetchGroups();
        } else {
            form.resetFields();
        }
    }, [visible]);

    const onFinish = async (values: any) => {
        const res = await createStudent(values);

        if (res) {
            message.success("Студента успішно додано");
            form.resetFields();
            hideModal();
        } else {
            message.error(errNotificationMessage);
        }
    };

    return (
        <Modal
            footer={null}
            title={"Додати студента"}
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
                    <Form.Item
                        rules={[{ required: true }]}
                        name={"groupId"}
                        label={"Група"}
                    >
                        <Select
                            options={groups.map(group => ({
                                value: group.id,
                                label: group.code
                            }))}
                            loading={groupsLoading}
                        />
                    </Form.Item>
                </div>
                <div style={{ display: "flex", justifyContent: "flex-end" }}>
                    <Popconfirm
                        title={"Ви впевнені що хочете створити студента ?"}
                        onConfirm={form.submit}
                        disabled={createUserLoading}
                    >
                        <Button loading={createUserLoading} type={"primary"}>
                            Створити
                        </Button>
                    </Popconfirm>
                </div>
            </Form>
        </Modal>
    );
};
export default CreateStudent;
