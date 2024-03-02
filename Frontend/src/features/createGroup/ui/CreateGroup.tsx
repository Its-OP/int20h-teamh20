import { FC, useEffect } from "react";
import { ModalData } from "../../../shared/model/adminTypes.ts";
import { Button, Form, Input, message, Modal, Popconfirm, Select } from "antd";
import { ModalKey } from "../../../pages/Admin/ui/Admin.tsx";
import { errNotificationMessage } from "../../../shared/model/const.ts";
import { useSubjects } from "../../../enteties/subject/api/useSubjects.ts";
import { useCreateGroup } from "../../../enteties/groups/api/useCreateGroup.ts";
const { useForm } = Form;
const CreateGroup: FC<ModalData> = ({ open, hideModal }) => {
    const visible = open === ModalKey.CreateGroup;
    const { subjects, fetchSubjects, subjectsLoading } = useSubjects();
    const { createGroup, createGroupLoading } = useCreateGroup();
    const [form] = useForm();

    useEffect(() => {
        if (visible) {
            fetchSubjects();
        } else {
            form.resetFields();
        }
    }, [visible]);
    const onFinish = async (data: any) => {
        const res = await createGroup(data);
        if (res) {
            message.success("Група створена успішно");
            form.resetFields();
            hideModal();
        } else {
            message.error(errNotificationMessage);
        }
    };

    return (
        <Modal
            footer={null}
            title={"Створення групи"}
            open={visible}
            onCancel={hideModal}
        >
            <Form form={form} onFinish={onFinish} layout={"vertical"}>
                <div style={{ display: "flex", gap: 40 }}>
                    <Form.Item name={"code"} label={"Назва"}>
                        <Input />
                    </Form.Item>
                    <Form.Item
                        style={{ width: "100%" }}
                        name={"subjectIds"}
                        label={"Предмети"}
                    >
                        <Select
                            style={{ width: "100%" }}
                            mode={"multiple"}
                            loading={subjectsLoading}
                            options={subjects.map(subj => ({
                                value: subj.id,
                                label: subj.title
                            }))}
                        />
                    </Form.Item>
                </div>
                <div style={{ display: "flex", justifyContent: "flex-end" }}>
                    <Popconfirm
                        title={"Ви впевнені що хочете створити групу ?"}
                        onConfirm={form.submit}
                        disabled={createGroupLoading}
                    >
                        <Button loading={createGroupLoading} type={"primary"}>
                            Створити
                        </Button>
                    </Popconfirm>
                </div>
            </Form>
        </Modal>
    );
};

export default CreateGroup;
