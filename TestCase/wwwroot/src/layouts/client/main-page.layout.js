import React, { Component } from 'react';
import Slider from '../../components/slider/slider.component';
import NewsList from '../../components/news/news-list/news-list.component';

class MainPage extends Component {
    render () {
        return (
            <div className="mainPage">
                <div className="row">
                    <div className="col-md-9 col-xs-12">
                    <Slider></Slider>
                    </div>
                    <div className="col-md-3 col-sm-3">
                        <img className="adv" src="https://tpc.googlesyndication.com/daca_images/simgad/3982838918109577790" alt="" />
                    </div>
                </div>
                <NewsList></NewsList>
            </div>
        )
    }
}

export default MainPage;