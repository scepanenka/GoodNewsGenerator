import React, {useEffect, useState} from 'react';
import {matchPath} from "react-router";
import Container from "@material-ui/core/Container";
import {API_BASE_URL} from "../../config";
import ArticleContent from "./ArticleContent";
import ArticleComments from "./ArticleComments";
import axios from "axios";
import s from "./ArticleComments/style.module.scss";
import {Button, TextField} from "@material-ui/core";
import {useUser} from "../../hooks/useUser";

const ArticleDetails = (props) => {

    const [article, setArticle] = useState([]);
    const [comments, setComments] = useState([]);
    const [newComment, setNewComment] = useState([]);
    const {user} = useUser();

    const match = matchPath(props.history.location.pathname, {
        path: '/news/:id',
        exact: true,
        strict: false
    });

    let articleId = match.params.id;


    useEffect(() => {
        axios.get(`${API_BASE_URL}/News/GetArticle/${articleId}`)
            .then(res => {
                setArticle(res.data)
            });
    }, []);

    useEffect(() => {
        fetchComments();
    }, []);

    const fetchComments = () => {
        axios.get(`${API_BASE_URL}/api/Comments/${articleId}`)
            .then(res => {
                setComments(res.data)
            });
    }


    function handleCommentChange(event) {
        setNewComment(event.target.value);
    };

    function handleFormSubmit(event) {
        event.preventDefault();
        setNewComment('');
        sendComment(newComment, user);
    };

    const sendComment = () => {
        axios.post(`${API_BASE_URL}/api/comments`, {
            articleId: articleId,
            email: user.email,
            content: newComment
        })
            .then(function (response) {
                console.log(response);
                fetchComments()
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    return (
        <Container>
            <ArticleContent article={article}/>
            <h3>Comments</h3>

            <form noValidate className={s.commentForm} onSubmit={handleFormSubmit}>
                <TextField
                    id="comment-input"
                    fullWidth
                    label="Напишите комментарий"
                    multiline
                    rows="2"
                    variant="outlined"
                    onChange={handleCommentChange}
                    value={newComment}
                />
                <Button type="submit"
                        fullWidth
                        variant="contained"
                        color="primary">
                    SEND
                </Button>
            </form>

            <ArticleComments comments={comments}/>
        </Container>
    )
}
export default ArticleDetails;
