import React, { Component }  from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { requestHeaders } from './../../const/headers.const';
import { serverPath } from './../../const/server-path.const';
import './slider.css';

class Slider extends Component {

    constructor(props) {
        super(props);
        this.state = {
            news : []
        }
    }

    componentDidMount(){
        axios.get(`${serverPath}/news/type/1`, requestHeaders)
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
            <div id="mainCarousel" className="carousel slide carousel-fade" data-ride="carousel">
                <div className="carousel-inner">
                    {
                        this.state.news.map((e, index) => {
                            return (
                                <div key={index} className={`carousel-item ${index === 0 ? 'active':'' }`}>
                                    <Link to={`/news-detail/${e._id}`}>
                                        <img className="d-block w-100" src={`${serverPath}/images/${e.image}`} alt={e.title} />
                                        <p className="slider-title">{e.title}</p>
                                    </Link>
                                </div>
                            );
                        })
                    }
                </div>
                <a className="carousel-control-prev" href="#mainCarousel" role="button" data-slide="prev">
                    <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span className="sr-only">Previous</span>
                </a>
                <a className="carousel-control-next" href="#mainCarousel" role="button" data-slide="next">
                    <span className="carousel-control-next-icon" aria-hidden="true"></span>
                    <span className="sr-only">Next</span>
                </a>
            </div>
        );
    }
}

export default Slider;