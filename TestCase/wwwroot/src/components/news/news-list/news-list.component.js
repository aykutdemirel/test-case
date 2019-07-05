import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { requestHeaders } from './../../../const/headers.const';
import { serverPath } from './../../../const/server-path.const';

import './news-list.css';

class NewsList extends Component {

    constructor(props) {
        super(props);
        this.state = {
            news : []
        }
    }

    componentDidMount(){
        axios.get(`${serverPath}/news/type/2`, requestHeaders)
        .then(response => {
            console.log('axios then | ', response); 
            this.setState({news: response.data});
        })
        .catch(error => {
            console.log('axios error | ',error)
        });
    }

    render () {
        return (
            <div className="row mt-md-4 haberler">
                {
                    this.state.news.map((e, index) => {
                        return (
                            <div className={`col-xs-3 col-sm-3 col-md-3 col-lg-3 news-index-${index}`} key={index}>
                                <div className="haberler-item">
                                    <Link to={`/news-detail/${e._id}`}>
                                        <div className="haberler-grid-div">
                                            <img className="haberler-grid-img" src={`${serverPath}/images/${e.image}`} alt={e.title} />
                                        </div>
                                        <p className="haberler-title">{e.title}</p>
                                        <p className="spot"></p>
                                    </Link>
                                </div>
                            </div>
                        );
                    })
                }
            </div>
        );
    }
}

export default NewsList;