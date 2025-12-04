import { useEffect } from "react";
import kapebaraLogo from "./kapebaralogo.png";

export default function Favicon() {
    useEffect(() => {
        const link = document.querySelector("link[rel*='icon']") || document.createElement("link");
        link.type = "image/png";
        link.rel = "icon";
        link.href = kapebaraLogo;
        document.getElementsByTagName("head")[0].appendChild(link);
    }, []);

    return null;
}
