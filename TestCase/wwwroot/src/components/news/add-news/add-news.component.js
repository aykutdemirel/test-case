import React, { Component } from 'react';
import CKEditor from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import EditableLabel from 'react-inline-editing';
import axios from 'axios';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import { requestHeaders } from './../../../const/headers.const';
import { newsTypes } from './../../../const/news-types.const';
import { serverPath } from './../../../const/server-path.const';

import './add-news.css';


class AddNews extends Component {

    constructor(props) {
        super(props);
        this.state = {
            news : {
                category:'',
                title:'Başlık Giriniz (Değiştirmek İçin Yazının Üzerine Tıklayınız)',
                subtitle:'Alt Başlık Giriniz (Değiştirmek İçin Yazının Üzerine Tıklayınız)',
                content: '',
                tags:[],
                isPublished:true,
                readCount:0,
                type:2,
                image:''
            },
            file:'',
            categories:[]
        }
    
        this.handleTypeChange = this.handleTypeChange.bind(this);
        this.handleCategoryChange = this.handleCategoryChange.bind(this);
        this.handleFocus = this.handleFocus.bind(this);
        this.handleFocusOut = this.handleFocusOut.bind(this);
        this.fileChanged = this.fileChanged.bind(this);
        //this.handleSubmit = this.handleSubmit.bind(this);
    }
 
    fileChanged(event) {
        event.persist();
        this.setState(state => ({
            file: event.target.files[0]
        }))
    }

    handleFocus(text) {
        console.log('Focused with text: ' + text);
    }
 
    handleFocusOut(text) {
        console.log('Left editor with text: ' + text);
    }      
    
    handleTypeChange(event) {
        event.persist();
        this.setState(state => ({
            news: {
                ...state.news,
                type: event.target.value
            }
        }))
    }

    handleCategoryChange(event) {
        event.persist();
        this.setState(state => ({
            news: {
                ...state.news,
                category: event.target.value
            }
        }))
    }

    componentDidMount(){
        axios.get(`${serverPath}/categories`, requestHeaders)
        .then(response => {
            console.log('axios then | ', response); 
            this.setState({categories: response.data});
        })
        .catch(error => {
            console.log('axios error | ',error)
        });
    }

    sendNews(){
        
        const formData = new FormData()
        formData.append('file',this.state.file);
        axios.post(`${serverPath}/images`,formData,{'content-type': 'multipart/form-data'}).then(imageResponse=>{
            const imageId = imageResponse.data._id;
            this.setState(state => ({
                news: {
                    ...state.news,
                    image: imageId
                }
            }))
        }).then(()=>{
            NotificationManager.success('Fotoğraf yüklendi, haber içeriği yükleniyor.', 'Haber Image', 5000);            
            axios.post(`${serverPath}/news`,this.state.news, requestHeaders)
            .then(response=>{
                console.log('axios then | ', response); 
                NotificationManager.success('Giriş yaptığınız haber kaydedilmek üzere sıraya alınmıştır.', 'Haber Kayıt', 5000);
            })
        })
        .catch(error => {
            NotificationManager.error(error, 'Beklenmeyen bir hata oluştu', 5000);
            console.log('axios error | ',error)
        });
    }

    render () {
        return (
            <div className="addNews">
                <div className="row">
                    <div className="col-md-12">
                        <div className="breadCrumb">
                            <div className="pull-left mr-md-2 mt-md-2">Haberler >></div>
                            <select className="select form-control col-md-2" value={this.state.news.category} onChange={this.handleCategoryChange}>
                                <option value="0">Kategori Seçiniz</option>
                                {
                                    this.state.categories.map((e, key) => {
                                        return <option key={key} value={e._id}>{e.name}</option>;
                                    })
                                }
                            </select>
                        </div>
                    </div>
                </div>
                <div className="row pt-md-4">
                    <div className="col-md-9 col-xs-12 white-coloumn p-md-5">

                        <div className="newsType row">
                            <label className="col-md-6 small-font">Haber Tipi Seçiniz</label>
                            <select className="col-md-6 select form-control input-xs" value={this.state.news.type} onChange={this.handleTypeChange}>
                                {
                                    newsTypes.map((e, key) => {
                                        return <option key={key} value={e.value}>{e.label}</option>;
                                    })
                                }
                            </select>
                        </div>

                        <hr></hr>
                        <div className="row">
                            <label className="col-md-4">Haber Fotoğrafı Giriniz</label>
                            <input className="col-md-8" type="file" onChange={this.fileChanged} />
                        </div>
                        <hr></hr>

                        <h1>
                            <EditableLabel 
                                text={this.state.news.title}
                                inputWidth='100%'
                                inputHeight='50px'
                                onFocus={(text)=>this.setState(state => ({
                                    news: {
                                        ...state.news,
                                        title: text
                                    }
                                }))}
                                onFocusOut={(text)=>this.setState(state => ({
                                    news: {
                                        ...state.news,
                                        title: text
                                    }
                                }))}
                            />
                        </h1>
                        <h2>
                            <EditableLabel 
                                text={this.state.news.subtitle}
                                inputWidth='100%'
                                inputHeight='50px'
                                onFocus={(text)=>this.setState(state => ({
                                    news: {
                                        ...state.news,
                                        subtitle: text
                                    }
                                }))}
                                onFocusOut={(text)=>this.setState(state => ({
                                    news: {
                                        ...state.news,
                                        subtitle: text
                                    }
                                }))}                          
                            />
                        </h2>
                        <CKEditor
                            editor={ ClassicEditor }
                            data="<p><strong>Lorem Ipsum</strong> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p><h2><strong>Where does it come from?</strong></h2><p>Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of 'de Finibus Bonorum et Malorum' (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, 'Lorem ipsum dolor sit amet..', comes from a line in section 1.10.32.</p><p>The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from 'de Finibus Bonorum et Malorum' by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.</p>"
                            onInit={ editor => {
                                // You can store the "editor" and use when it is needed.
                                this.setState(state => ({
                                    news: {
                                        ...state.news,
                                        content: editor.getData()
                                    }
                                }))
                            } }
                            onChange={ ( event, editor ) => {
                                this.setState(state => ({
                                    news: {
                                        ...state.news,
                                        content: editor.getData()
                                    }
                                }))
                            } }
                            onBlur={ editor => {
                                console.log( 'Blur.', editor );
                            } }
                            onFocus={ editor => {
                                console.log( 'Focus.', editor );
                            } }
                        />
                        <div className="clearfix mt-md-5">
                            <button className="btn btn-danger pull-right" onClick={()=>this.sendNews()}>
                                Haberi Kaydet
                            </button>
                        </div>
                    </div>
                    <div className="col-md-3 col-sm-3">
                        <img className="adv" alt="adv" src="https://tpc.googlesyndication.com/daca_images/simgad/3982838918109577790" />
                    </div>
                </div>
                <NotificationContainer/>
            </div>
        );
    }
}

export default AddNews;