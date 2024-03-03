import { FC, useEffect, useState } from "react";
import { ModalData } from "../../../shared/model/adminTypes.ts";
import {
    Button,
    Checkbox,
    Form,
    Input,
    message,
    Modal,
    Popconfirm,
    Select
} from "antd";
import { ModalKey } from "../../../pages/Admin/ui/Admin.tsx";
import { useGroups } from "../../../enteties/groups/api/useGroups.ts";
import FormItem from "antd/es/form/FormItem";
import { useCreateNotification } from "../../../enteties/notification/api/useCreateNotification.ts";
import { useStudents } from "../../../enteties/students/api/useStudents.ts";
import { useTemplatesNotification } from "../../../enteties/notification/api/useTemplatesNotifications.ts";
import { useCreateTemplateNotification } from "../../../enteties/notification/api/createTemplateNotification.ts";
import { errNotificationMessage } from "../../../shared/model/const.ts";

const { useForm } = Form;
const CreateNotification: FC<ModalData> = ({ open, hideModal }) => {
    const visible = open === ModalKey.CreateNotification;
    const [form] = useForm();

    const { createNotification, createNotyLoading } = useCreateNotification();
    const {
        templatesNotification,
        fetchTemplatesNotification,
        templateNotificationLoading
    } = useTemplatesNotification();

    const { groups, groupsLoading, fetchGroups } = useGroups();

    const { students, fetchStudents, studentsLoading, clearStudents } =
        useStudents();

    const { createTemplateNotification } = useCreateTemplateNotification();

    const [group, setGroup] = useState<string>();
    const [templateId, setTemplateId] = useState<number>();

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

    useEffect(() => {
        if (visible) {
            fetchGroups();
            fetchTemplatesNotification();
        } else {
            form.resetFields();
        }
    }, [visible]);
    const onFinish = async (data: {
        title: string;
        text: string;
        receiverIds: number[];
        savetemplate: boolean;
    }) => {
        console.log(data);
        const ids = data.receiverIds
            ? data.receiverIds
            : students.map(std => std.id);

        const body = { title: data.title, text: data.text, receiverIds: ids };
        const res = await createNotification(body);
        if (res) {
            message.success("Група створена успішно");
            form.resetFields();
            if (data.savetemplate) {
                const res = await createTemplateNotification({
                    text: data.text,
                    title: data.title
                });
                if (res) {
                    message.success("Шаблон створено успішно");
                } else {
                    message.error(errNotificationMessage);
                }
            }
            hideModal();
        } else {
            message.error(errNotificationMessage);
        }
    };

    useEffect(() => {
        if (templateId) {
            const template = templatesNotification.find(
                temp => temp.id === templateId
            );

            form.setFieldsValue(template);
        } else {
            form.resetFields();
        }
    }, [templateId]);

    return (
        <Modal
            footer={null}
            title={"Створення групи"}
            open={visible}
            onCancel={hideModal}
        >
            <div
                style={{
                    display: "grid",
                    gridTemplateColumns: "1fr 1fr",
                    gap: 40
                }}
            >
                <Select
                    options={groups.map(group => ({
                        value: group.id,
                        label: group.code
                    }))}
                    onChange={setGroup}
                    value={group}
                    placeholder={"Група"}
                    style={{ minWidth: 200 }}
                    loading={groupsLoading}
                    allowClear
                />
                <Select
                    options={templatesNotification.map(temp => ({
                        value: temp.id,
                        label: temp.title
                    }))}
                    onChange={setTemplateId}
                    value={templateId}
                    placeholder={"Шаблон"}
                    style={{ minWidth: 200 }}
                    loading={templateNotificationLoading}
                    allowClear
                />
            </div>

            <Form form={form} onFinish={onFinish} layout={"vertical"}>
                <FormItem name={"receiverIds"} label={"Студенти"}>
                    <Select
                        options={students.map(stud => ({
                            value: stud.id,
                            label: `${stud.lastname} ${stud.firstname}`
                        }))}
                        mode={"multiple"}
                        style={{ minWidth: 200 }}
                        loading={studentsLoading}
                        allowClear
                    />
                </FormItem>
                <FormItem name={"title"} label={"Тема"}>
                    <Input />
                </FormItem>
                <FormItem name={"text"} label={"Текст повідомлення"}>
                    <Input.TextArea />
                </FormItem>
                <FormItem
                    name={"savetemplate"}
                    valuePropName={"checked"}
                    label={"Зберегти як шаблон"}
                >
                    <Checkbox disabled={templateId !== undefined} />
                </FormItem>
                <div style={{ display: "flex", justifyContent: "flex-end" }}>
                    <Popconfirm
                        title={"Ви впевнені що хочете створити розсилку ?"}
                        onConfirm={form.submit}
                        disabled={createNotyLoading}
                    >
                        <Button loading={createNotyLoading} type={"primary"}>
                            Створити
                        </Button>
                    </Popconfirm>
                </div>
            </Form>
        </Modal>
    );
};

export default CreateNotification;
