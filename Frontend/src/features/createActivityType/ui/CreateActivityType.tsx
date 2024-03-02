import { FC } from "react";
import { ModalData } from "../../../shared/model/adminTypes.ts";
import {
    Button,
    Checkbox,
    Form,
    Input,
    message,
    Modal,
    Popconfirm
} from "antd";
import { ModalKey } from "../../../pages/Admin/ui/Admin.tsx";
import useCreateSubject from "../../../enteties/subject/api/useCreateSubject.ts";
import { errNotificationMessage } from "../../../shared/model/const.ts";
const { useForm } = Form;
const CreateActivityType: FC<ModalData> = ({ open, hideModal }) => {
    const visible = open === ModalKey.CreateDiscipline;
    const { createSubject, subjectCreateLoading } = useCreateSubject();

    const [form] = useForm();

    const onFinish = async (data: any) => {
        const res = await createSubject(data);
        if (res) {
            message.success("");
            form.resetFields();
            hideModal();
        } else {
            message.error(errNotificationMessage);
        }
    };

    return (
        <Modal
            footer={null}
            title={"Створення предмету"}
            open={visible}
            onCancel={hideModal}
        >
            <Form form={form} onFinish={onFinish} layout={"vertical"}>
                <div style={{ display: "flex", gap: 40 }}>
                    <Form.Item name={"title"} label={"Назва"}>
                        <Input />
                    </Form.Item>
                    <Form.Item name={"isExam"} label={"Форма заліку екзамен"}>
                        <Checkbox />
                    </Form.Item>
                </div>
                <div style={{ display: "flex", justifyContent: "flex-end" }}>
                    <Popconfirm
                        title={"Ви впевнені що хочете створити активність ?"}
                        onConfirm={form.submit}
                        disabled={subjectCreateLoading}
                    >
                        <Button loading={subjectCreateLoading} type={"primary"}>
                            Створити
                        </Button>
                    </Popconfirm>
                </div>
            </Form>
        </Modal>
    );
};

export default CreateActivityType;
