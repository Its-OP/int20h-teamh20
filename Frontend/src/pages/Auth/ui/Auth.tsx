import { Button, Form, Input, Card, Typography } from "antd";
import { Helmet } from "react-helmet";
import { useForm } from "antd/es/form/Form";
import { BASE_TITLE } from "../../../shared/model/const.ts";
import { useAuth } from "../../../features/auth/model/useAuth.ts";

type FieldType = {
    email?: string;
    password?: string;
};

const Auth = () => {
    const { signIn } = useAuth();

    const [form] = useForm();

    const logIn = async () => {
        await signIn(form.getFieldsValue());
    };

    return (
        <>
            <Helmet>{BASE_TITLE} | Авторизація</Helmet>
            <div
                style={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    height: "100%"
                }}
            >
                <Card>
                    <Typography.Title level={2} style={{ textAlign: "center" }}>
                        Авторизація
                    </Typography.Title>
                    <Form
                        onFinish={() => {}}
                        form={form}
                        labelCol={{ span: 8 }}
                        wrapperCol={{ span: 24 }}
                        style={{ maxWidth: 600 }}
                        initialValues={{ remember: true }}
                        layout={"vertical"}
                        autoComplete='off'
                    >
                        <Form.Item<FieldType>
                            label='Логін'
                            name='email'
                            rules={[
                                {
                                    required: true,
                                    message: "Будь ласка введіть логін!"
                                }
                            ]}
                        >
                            <Input />
                        </Form.Item>

                        <Form.Item<FieldType>
                            label='Пароль'
                            name='password'
                            rules={[
                                {
                                    required: true,
                                    min: 6,
                                    max: 12,
                                    message: "Пароль має містити 6-12 символів!"
                                }
                            ]}
                        >
                            <Input.Password />
                        </Form.Item>

                        <Form.Item wrapperCol={{ span: 24 }}>
                            <div
                                style={{
                                    width: "100%",
                                    display: "flex",
                                    gap: 10
                                }}
                            >
                                <Button
                                    type='primary'
                                    style={{ flex: 1 }}
                                    onClick={logIn}
                                    htmlType={"submit"}
                                >
                                    signIn
                                </Button>
                            </div>
                        </Form.Item>
                    </Form>
                </Card>
            </div>
        </>
    );
};

export default Auth;
