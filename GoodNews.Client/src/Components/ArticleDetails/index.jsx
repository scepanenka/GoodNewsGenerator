import React, {useEffect, useState} from 'react';
import {matchPath} from "react-router";
import Container from "@material-ui/core/Container";
import parse from 'html-react-parser';
import {API_BASE_URL} from "../../config";
import Login from "../Login";

const ArticleDetails = (props) => {

    const [hasError, setErrors] = useState(false);
    const [article, setArticle] = useState([]);

    const match = matchPath(props.history.location.pathname, {
        path: '/news/:id',
        exact: true,
        strict: false
    });

    let articleId = match.params.id;


    useEffect(() => {
        async function fetchData() {
            //const res = await fetch(`https://good-news-server.azurewebsites.net/api/News/${articleId}`);
            const res = await fetch(`${API_BASE_URL}/api/News/${articleId}`);
            res
                .json()
                .then(res => setArticle(res))
                .catch(err => setErrors(err));
        }

        fetchData();
    }, []);


    return (<Container align="justify">
        <h1>{article.title}</h1>
        {parse(`${article.content}`)}
    </Container>)
}
export default ArticleDetails;
