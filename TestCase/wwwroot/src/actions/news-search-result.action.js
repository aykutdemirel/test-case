export const SEARCH_NEWS = 'search:news';
export const ADD_VISITED_NEWS = 'visited:news';

export function searchNews (title) {
    return {
        type: SEARCH_NEWS,
        payload: {
            text: title,
        }
    }
}

export function addVisitedNews ({title, id}) {
    return {
        type: ADD_VISITED_NEWS,
        payload: {
            title: title,
            id: id
        }
    }
}
