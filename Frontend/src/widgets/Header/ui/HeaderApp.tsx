import { FC } from "react";
import { Header } from "antd/es/layout/layout";
import { Badge, Button, Menu } from "antd";
import { links } from "../model/links.ts";
import MenuItem from "antd/es/menu/MenuItem";
import { Link, useLocation } from "react-router-dom";
import { UserOutlined, BellOutlined } from "@ant-design/icons";
import { useAppDispatch, useAppSelector } from "../../../App/appStore.ts";
import { notificationSlice } from "../../../enteties/notification/model/notificationSlice.ts";
const HeaderApp: FC = () => {
    const { pathname } = useLocation();

    const { userId } = useAppSelector(state => state.user);
    const { amount } = useAppSelector(state => state.notifications);

    const dispatch = useAppDispatch();

    return (
        <Header style={{ backgroundColor: "#FFF", display: "flex", gap: 40 }}>
            <Menu
                style={{ flex: 1 }}
                selectedKeys={[pathname]}
                theme={"light"}
                mode={"horizontal"}
            >
                {links.map(item => (
                    <MenuItem key={item.path}>
                        <Link to={item.path}>{item.title}</Link>
                    </MenuItem>
                ))}
            </Menu>
            <Menu theme={"light"} mode={"horizontal"} selectedKeys={[]}>
                <MenuItem>
                    <Badge count={amount}>
                        <Button
                            onClick={() =>
                                dispatch(
                                    notificationSlice.actions.showNotification()
                                )
                            }
                            type={"primary"}
                        >
                            <BellOutlined />
                        </Button>
                    </Badge>
                </MenuItem>
                <MenuItem>
                    <Link to={"/student/" + userId}>
                        <Button type={"primary"}>
                            <UserOutlined />
                        </Button>
                    </Link>
                </MenuItem>
            </Menu>
        </Header>
    );
};

export default HeaderApp;
