import React, {useEffect, useState} from "react";

const News = (props) => {

    const [hasError, setErrors] = useState(false);
    const [news, setNews] = useState([]);

    useEffect(() => {
        async function fetchData() {
            const res = await fetch("https://localhost:44317/api/News");
            res
                .json()
                .then(res => setNews(res))
                .catch(err => setErrors(err));
        }

        fetchData();
    }, []);

    return (
        <div>
                { news.map(article =>
                    <p key={article.id}>{article.title}</p>
                )}

            <hr/>
        </div>
    );
};
export default News;
