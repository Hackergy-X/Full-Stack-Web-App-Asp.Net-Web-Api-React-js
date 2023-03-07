import React from "react";
import AdminHeader from "./AdminHeader";
import axios from "axios";

function AdminDashboard() {

    const [news, setNews] = React.useState([]);

    React.useEffect(() => {
        const url = "https://localhost:7226/api/News/GetAllNews";
        axios.get(url).then((response) => {
            const result = response.data;
            if (result.statusCode === 200) {
                setNews(result.news);
            }
        });
    }, []);

    

    return (
        <div>
            <AdminHeader />

        </div>
    );
}


export default AdminDashboard;