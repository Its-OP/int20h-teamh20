import { FC } from "react";
import { Button, message } from "antd";
import { CopyOutlined } from "@ant-design/icons";

export const CopiIcon: FC<{ data: string | number }> = ({ data }) => {
    function copyTextToClipboard() {
        navigator.clipboard.writeText(data.toString()).then(
            function () {
                message.success("Скопійовано в буффер");
            },
            function () {
                message.error("Виникла помилка копіювання");
            }
        );
    }
    return (
        <Button
            style={{ margin: "0 5px" }}
            onClick={copyTextToClipboard}
            size={"small"}
        >
            <CopyOutlined />
        </Button>
    );
};
