import React, { Component } from 'react';
import  { Link } from 'react-router-dom';
import SearchBox from './../searchbox/searchbox.component';

import './header.css';

class Header extends Component {

    constructor(props) {
        super(props);
        this.state = {
        }
    }


    render () {
        return (
            <div className="navbar-wrapper navbar-fixed-top">
                <nav className="navbar navbar-expand-lg navbar-light bg-light">
                    <div className="container">
                        <Link to="/" className="navbar-brand">Demirören Haberler</Link>
                        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse" id="navbarSupportedContent">
                            <ul className="navbar-nav mr-auto">
                                <li className="nav-item active">
                                    <Link to="/" className="nav-link">Anasayfa <span className="sr-only">(current)</span></Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/" className="nav-link">Son Dakika</Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/" className="nav-link">Spor</Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/" className="nav-link">Ekonomi</Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/" className="nav-link">Finans</Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/" className="nav-link">Magazin</Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/admin/add-news" className="nav-link"><i className="fa fa-plus"></i>Haber Ekle</Link>
                                </li>
                            </ul>
                            <SearchBox></SearchBox>
                        </div>
                    </div>
                </nav>
            </div>
        );
    }
}

export default Header;