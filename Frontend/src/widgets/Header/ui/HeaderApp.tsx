import { FC } from "react";
import { Header } from "antd/es/layout/layout";
import { Menu } from "antd";
import { links } from "../model/links.ts";
import MenuItem from "antd/es/menu/MenuItem";
import { Link, useLocation } from "react-router-dom";

const HeaderApp: FC = () => {
    const { pathname } = useLocation();

    console.log(pathname);

    return (
        <Header>
            <Menu selectedKeys={[pathname]} theme={"dark"} mode={"horizontal"}>
                {links.map(item => (
                    <MenuItem key={item.path}>
                        <Link to={item.path}>{item.title}</Link>
                    </MenuItem>
                ))}
            </Menu>
        </Header>
    );
};

export default HeaderApp;
