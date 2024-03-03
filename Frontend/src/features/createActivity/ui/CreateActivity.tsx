import { FC, useEffect, useState } from "react";
import { ModalData } from "../../../shared/model/adminTypes.ts";
import {
    Button,
    Checkbox,
    DatePicker,
    Form,
    Input,
    message,
    Modal,
    Popconfirm,
    Select,
    Table,
    TableProps
} from "antd";
import { ModalKey } from "../../../pages/Admin/ui/Admin.tsx";
import { useGroups } from "../../../enteties/groups/api/useGroups.ts";
import { useCreateActivity } from "../../../enteties/activity/api/useCreateActivity.ts";
import { useStudents } from "../../../enteties/students/api/useStudents.ts";
import { StudentSimple } from "../../../shared/model/types.ts";
import { useSubjects } from "../../../enteties/subject/api/useSubjects.ts";
import { useActiviesType } from "../../../enteties/activity/api/useActiviesType.ts";
import { errNotificationMessage } from "../../../shared/model/const.ts";

const { useForm } = Form;
const CreateActivity: FC<ModalData> = ({ open, hideModal }) => {
    const visible = open === ModalKey.CreateActivity;
    const { groups, fetchGroups, groupsLoading } = useGroups();
    const { createActivity, createActivityLoading } = useCreateActivity();
    const { students, fetchStudents } = useStudents();
    const { subjects, fetchSubjects } = useSubjects();
    const { activityTypes, fetchActivityTypes } = useActiviesType();
    const [groupId, setGroupId] = useState<number>();
    const [data, setData] = useState<any[]>([]);

    const [form] = useForm();

    useEffect(() => {
        if (visible) {
            fetchGroups();
            fetchActivityTypes();
        } else {
            form.resetFields();
        }
    }, [visible]);

    useEffect(() => {
        if (groupId) {
            fetchStudents(groupId);
            fetchSubjects(groupId);
        }
    }, [groupId]);
    const onFinish = async (formData: any) => {
        const res = await createActivity({ ...formData, body: data });
        if (res) {
            message.success("Група створена успішно");
            form.resetFields();
            hideModal();
        } else {
            message.error(errNotificationMessage);
        }
    };

    const prepareData = (studs: StudentSimple[]) =>
        studs.map(stud => ({
            studentId: stud.id,
            name: `${stud.lastname} ${stud.firstname} ${stud.patronymic}`,
            isAbsent: false,
            score: 0
        }));

    useEffect(() => {
        setData(prepareData(students));
    }, [students]);
    const absenteHandler = (id: number, value: boolean) => {
        setData(prev => {
            return prev.map(item => {
                if (item.studentId === id) {
                    item.isAbsent = value;
                }

                return item;
            });
        });
    };

    const scoreHandler = (id: number, value: string) => {
        setData(prev => {
            return prev.map(item => {
                if (item.studentId === id) {
                    item.score = value;
                }

                return item;
            });
        });
    };

    const columns: TableProps["columns"] = [
        {
            dataIndex: "name",
            title: "ПІБ"
        },
        {
            dataIndex: "isAbsent",
            title: "Присутність",
            render: (value, record: any) => {
                return (
                    <Checkbox
                        onChange={e =>
                            absenteHandler(record.studentId, e.target.checked)
                        }
                        checked={!value}
                    />
                );
            }
        },
        {
            dataIndex: "score",
            title: "Бал",
            render: (value, record: any) => {
                return (
                    <Input
                        onChange={e =>
                            scoreHandler(record.studentId, e.target.value)
                        }
                        value={value}
                    />
                );
            }
        }
    ];

    return (
        <Modal
            footer={null}
            title={"Створення активності"}
            open={visible}
            onCancel={hideModal}
        >
            <Form form={form} onFinish={onFinish} layout={"vertical"}>
                <div style={{ display: "flex", gap: 40 }}>
                    <Form.Item
                        style={{ width: "100%" }}
                        name={"subjectIds"}
                        label={"Группи"}
                    >
                        <Select
                            style={{ width: "100%" }}
                            loading={groupsLoading}
                            value={groupId}
                            onChange={setGroupId}
                            options={groups.map(subj => ({
                                value: subj.id,
                                label: subj.code
                            }))}
                        />
                    </Form.Item>
                    <Form.Item name={"dateTime"} label={"Дата та час"}>
                        <DatePicker showTime />
                    </Form.Item>
                </div>

                <Form.Item name={"maxScore"} label={"Максимальний бал"}>
                    <Input />
                </Form.Item>
                <div style={{ display: "flex", gap: 40 }}>
                    <Form.Item
                        style={{ width: "100%" }}
                        name={"subjectIds"}
                        label={"Предмети"}
                    >
                        <Select
                            style={{ width: "100%" }}
                            loading={groupsLoading}
                            options={subjects.map(subj => ({
                                value: subj.id,
                                label: subj.title
                            }))}
                        />
                    </Form.Item>
                    <Form.Item
                        style={{ width: "100%" }}
                        name={"activityTypeId"}
                        label={"Тип активності"}
                    >
                        <Select
                            style={{ width: "100%" }}
                            loading={groupsLoading}
                            options={activityTypes.map(subj => ({
                                value: subj.id,
                                label: subj.title
                            }))}
                        />
                    </Form.Item>
                </div>
                <Table pagination={false} dataSource={data} columns={columns} />
                <div style={{ display: "flex", justifyContent: "flex-end" }}>
                    <Popconfirm
                        title={"Ви впевнені що хочете створити групу ?"}
                        onConfirm={form.submit}
                        disabled={createActivityLoading}
                    >
                        <Button
                            loading={createActivityLoading}
                            type={"primary"}
                        >
                            Створити
                        </Button>
                    </Popconfirm>
                </div>
            </Form>
        </Modal>
    );
};

export default CreateActivity;
