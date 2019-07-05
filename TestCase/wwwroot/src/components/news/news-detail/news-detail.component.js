import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { requestHeaders } from './../../../const/headers.const';
import { serverPath } from './../../../const/server-path.const';

import './news-detail.css';


class NewsDetail extends Component {

    constructor(props) {
        super(props);
        this.state = {
            news : {},
            category : {}
        }
    }

    componentDidMount(){
        axios.get(`${serverPath}/news/`+this.props.props.match.params.id, requestHeaders)
        .then(response => {
            this.setState({news: response.data});
        })
        .then(()=>{
            axios.get(`${serverPath}/categories/`+this.state.news.category, requestHeaders)
            .then(response => {
                this.setState({category: response.data});
            })
        })
        .catch(error => {
            console.log('axios error | ',error)
        });
    }

    render () {
        return (
            <div className="detail-news-wrapper">
                <div className="row">
                    <div className="col-md-12">
                        <div className="breadCrumb">
                            <div className="pull-left mr-md-2 mt-md-2">
                                <Link to="/">Haberler</Link>>>
                                <Link to={`/category/${this.state.category._id}`}>{this.state.category.name}</Link>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row pt-md-4">
                    <div className="col-md-9 col-xs-12 white-coloumn p-md-5">
                        <h1>{this.state.news.title}</h1>
                        <div className="mb-md-5 mt-md-4">
                            <img className="news-detail-main-img" src={`${serverPath}/images/${this.state.news.image}`} alt={this.state.news.title} />
                        </div>
                        <h2>{this.state.news.subtitle}</h2>
                        <div dangerouslySetInnerHTML={{__html: this.state.news.content}}>
                        </div>
                    </div>
                    <div className="col-md-3 col-sm-3">
                        <img className="adv" alt="adv" src="https://tpc.googlesyndication.com/daca_images/simgad/3982838918109577790" />
                    </div>
                </div>
            </div>
        );
    }
}

export default NewsDetail;