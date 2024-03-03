import { ModalKey } from "../../pages/Admin/ui/Admin.tsx";

export type ModalData = {
    open: ModalKey;
    hideModal: () => void;
};
