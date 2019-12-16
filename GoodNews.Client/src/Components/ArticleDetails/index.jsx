import React, {useEffect, useState} from 'react';

const ArticleDetails = (props) => {

    const [hasError, setErrors] = useState(false);
    const [article, setArticle] = useState([]);
    const {articleId} = props.match.params;

    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`https://localhost:44317/api/news/5DAE7B8F-CC8A-4176-6FFC-08D78202DDFA`);
            res
                .json()
                .then(res => setArticle(res))
                .catch(err => setErrors(err));
        }
        fetchData();
    }, []);

    const getContent = () => {

    }

    return (
        <div id="articleContent">
            <h1>{article.title}</h1>
            <div>{article.text}</div>
        </div>
    )
}
 export default ArticleDetails;
