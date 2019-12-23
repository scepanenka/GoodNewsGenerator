import React, {useEffect, useState} from "react";
import s from './style.module.scss'
import {Container, TextField, Button, Card} from "@material-ui/core";


const ArticleComments = (props) => {

    return (
        <div>
            {props.comments
                ?
            <div >
                {props.comments.map(comment =>
                    <Card key={comment.id} className={s.comment}>
                        <div className={s.title}>
                        <small><strong>{comment.user.userName}</strong></small>
                        <small>{comment.date}</small>
                        </div>
                        <p className={s.commentContent}><small>{comment.content}</small></p>
                    </Card>
                )}
            </div>
                :
                <p>NO COMMENTS</p>
                }
        </div>
    )
}

export default ArticleComments
